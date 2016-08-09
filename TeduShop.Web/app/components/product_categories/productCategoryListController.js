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
        '$scope', 'apiService', function($scope, apiService) {
            $scope.productCategories = [];
            $scope.page = 0;
            $scope.pagesCount = 0;

            $scope.getProductCategories = function getProductCategories(page) {

                page = page || 0;
                // config paramester for url
                var config = {
                    params: {
                        page: page,
                        pageSize: 2
                    }
                }
                // config paramester for url

                apiService.get('/api/productcategory/getall', config,
                    function(result) {
                        $scope.productCategories = result.data.Items;
                        $scope.page = result.data.Page;
                        $scope.pagesCount = result.data.TotalPages;
                        $scope.totalCount = result.data.TotalCount;
                    },
                    function() {
                        console.log('Load productcategory failed.');
                    });
            }

            $scope.getProductCategories();
        }
    ]);
})(angular.module('tedushop.products_category'));