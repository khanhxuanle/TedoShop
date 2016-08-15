(function () {
    angular.module('tedushop.products', ['tedushop.common']).config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        $stateProvider
                .state('products',
                {
                    url: "/products",
                    parent: 'base',
                    templateUrl: "/app/components/products/productListView.html",
                    controller: "productListController"
                })
                .state('products_add',
                {
                    url: "/products_add",
                    parent: 'base',
                    templateUrl: "/app/components/products/productAddView.html",
                    controller: "productAddController"
                })
                .state('product_edit',
                {
                    url: "/product_edit/:id",
                    parent: 'base',
                    templateUrl: "/app/components/products/productEditView.html",
                    controller: "productEditController"
                });
    }]);
})();