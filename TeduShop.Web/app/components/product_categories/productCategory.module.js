//(function () {
//    angular.module('tedushop.products_category', ['tedushop.common']).config(config);

//    config.$inject = ['$stateProvider', '$urlRouterProvider'];

//    function config($stateProvider, $urlRouterProvider) {

//        $stateProvider
//            .state('products_category',
//            {
//                url: "/products_category",
//                templateUrl: "/app/components/product_categories/productCategoryListView.html",
//                controller: "productCategoryListController"
//            });
//    }
//})();

(function () {
    angular.module('tedushop.products_category', ['tedushop.common']).config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('products_category',
            {
                url: "/products_category",
                templateUrl: "/app/components/product_categories/productCategoryListView.html",
                controller: "productCategoryListController"
            })
            .state('add_product_category',
            {
                url: "/add_product_category",
                templateUrl: "/app/components/product_categories/productCategoryAddView.html",
                controller: "productCategoryAddController"
            })
            .state('edit_product_category',
            {
                url: "/edit_product_category/:id",
                templateUrl: "/app/components/product_categories/productCategoryEditView.html",
                controller: "productCategoryEditController"
            });
    }]);
})();