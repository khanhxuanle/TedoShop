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
        '$scope', 'apiService', 'notificationService', function ($scope, apiService, notificationService) {
            $scope.productCategories = [];
            $scope.page = 0;
            $scope.pagesCount = 0;
            $scope.keywords = '';

            $scope.getProductCategories = function (page, enableNotification) {
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
        }
    ]);
})(angular.module('tedushop.products_category'));