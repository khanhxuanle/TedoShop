(function (app) {
    app.controller('productListController',
    [
        '$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', function ($scope, apiService, notificationService, $ngBootbox, $filter) {
            $scope.products = [];
            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.keywords = '';

            $scope.getProducts = function (page, enableNotification) {
                $scope.checkedAdd = false;
                $scope.isAll = false;
                page = page || 0;
                // config paramester for url
                var config = {
                    params: {
                        keyword: $scope.keywords,
                        page: page,
                        pageSize: 2
                    }
                }
                // config paramester for url

                apiService.get('/api/product/getall',
                    config,
                    function (result) {
                        if (result.data.TotalCount == 0) {
                            notificationService.displayWarning("Không có bản ghi nào được tìm thấy.");
                        } else {
                            if (enableNotification) {
                                notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                            }
                        }
                        $scope.products = result.data.Items;
                        $scope.page = result.data.Page;
                        $scope.pagesCount = result.data.TotalPages;
                        $scope.totalCount = result.data.TotalCount;
                    },
                    function () {
                        console.log('Load product failed.');
                    });
            };

            $scope.getProducts(0, true);

            $scope.searchKeyword = function () {
                $scope.getProducts(0, true);
            };

            $scope.deleteProduct = function (id) {
                $ngBootbox.confirm('Bạn có muốn xóa không?')
                    .then(function () {
                        var config = {
                            params: {
                                id: id
                            }
                        }
                        apiService.del('/api/product/del',
                            config,
                            function (result) {
                                notificationService.displaySuccess('Xoá thành công');
                                $scope.getProducts(0, true);
                            },
                            function () {
                                notificationService.displayError('Xoá không thành công');
                            });
                    });
            }

            $scope.checked = true;
            $scope.isAll = false;

            $scope.$watch("products", function (n, o) {
                var checked = $filter("filter")(n, { checked: true });
                if (checked.length) {
                    $scope.selected = checked;
                    $scope.checked = false;
                } else {
                    $scope.checked = true;
                }
            }, true); 

            $scope.selectAll = function () {
                if ($scope.isAll === false) {
                    angular.forEach($scope.products, function (item) {
                        item.checked = true;
                    });
                    $scope.isAll = true;
                } else {
                    angular.forEach($scope.products, function (item) {
                        item.checked = false;
                    });
                    $scope.isAll = false;
                }
            }

            $scope.deleteMutiple = function () {
                $ngBootbox.confirm('Bạn có muốn xóa không?')
                    .then(function() {
                        var listId = [];
                        $.each($scope.selected,
                            function (i, item) {
                                listId.push(item.ID);
                            });
                        var config = {
                            params: {
                                checkedProduct: JSON.stringify(listId)
                            }
                        }
                        apiService.del('/api/product/delmutile',
                            config,
                            function (result) {
                                notificationService.displaySuccess('Xoá thành công' + result.data + 'bản ghi');
                                $scope.getProducts(0, true);
                            },
                            function (error) {
                                notificationService.displayError('Xoá không thành công');
                                $scope.getProducts(0, true);
                            });
                    });    
            }
        }
    ]);
})(angular.module('tedushop.products'));