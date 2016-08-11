//(function (app) {
//    app.controller('productCategoryListController', productCategoryListController);

//    productCategoryListController.$inject = ['$scope', 'apiService'];

//    function productCategoryListController($scope, apiService) {
//        $scope.productCategories = [];

//        $scope.getProductCategories = function getProductCategories() {
//            apiService.get('/api/productcategory/getall',
//                null,
//                function(result) {
//                    $scope.productCategories = result.data;
//                },
//                function() {
//                    console.log('Load productcategory failed.');
//                });
//        }

//        $scope.getProductCategories();
//    }
//})(angular.module('tedushop.products_category'))

(function(app) {
    app.controller('productCategoryListController',
    [
        '$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', function ($scope, apiService, notificationService, $ngBootbox, $filter) {
            $scope.productCategories = [];
            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.keywords = '';

            $scope.getProductCategories = function (page, enableNotification) {
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

                apiService.get('/api/productcategory/getall',
                    config,
                    function (result) {
                        if (result.data.TotalCount == 0) {
                            notificationService.displayWarning("Không có bản ghi nào được tìm thấy.");
                        } else {
                            if (enableNotification) {
                                notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.');
                            }                                                    
                        }
                        $scope.productCategories = result.data.Items;
                        $scope.page = result.data.Page;
                        $scope.pagesCount = result.data.TotalPages;
                        $scope.totalCount = result.data.TotalCount;
                    },
                    function() {
                        console.log('Load productcategory failed.');
                    });
            };

            $scope.getProductCategories(0, true);

            $scope.searchKeyword = function() {
                $scope.getProductCategories(0, true);
            };

            $scope.deleteProductCategory = function(id) {
                $ngBootbox.confirm('Bạn có muốn xóa không?')
                    .then(function() {
                        var config = {
                            params: {
                                id: id
                            }
                        }
                        apiService.del('/api/productcategory/del',
                            config,
                            function (result) {
                                notificationService.displaySuccess('Xoá thành công');
                                $scope.getProductCategories(0, true);
                            },
                            function() {
                                notificationService.displayError('Xoá không thành công');
                            });
                    });
            }

            $scope.checked = true;
            $scope.isAll = false;

            $scope.$watch("productCategories", function (n, o) {
                var checked = $filter("filter")(n, { checked: true });
                if (checked.length) {
                    $scope.selected = checked;
                    $scope.checked = false;
                } else {
                    $scope.checked = true;
                }
            }, true);

            $scope.selectAll = function() {
                if ($scope.isAll === false) {
                    angular.forEach($scope.productCategories, function (item) {
                        item.checked = true;
                    });
                    $scope.isAll = true;
                } else {
                    angular.forEach($scope.productCategories, function (item) {
                        item.checked = false;
                    });
                    $scope.isAll = false;
                }
            }

            $scope.deleteMutiple = function () {
                var listId = [];
                $.each($scope.selected,
                    function(i, item) {
                        listId.push(item.ID);
                    });
                var config = {
                    params: {
                        checkedProductCategories: JSON.stringify(listId)
            }
                }
                apiService.del('/api/productcategory/delmutile',
                    config,
                    function(result) {
                        notificationService.displaySuccess('Xoá thành công' + result.data + 'bản ghi');
                        $scope.getProductCategories(0, true);
                    },
                    function(error) {
                        notificationService.displayError('Xoá không thành công');
                        $scope.getProductCategories(0, true);
                    });
            }
        }
    ]);
})(angular.module('tedushop.products_category'));