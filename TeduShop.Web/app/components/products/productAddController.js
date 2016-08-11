//(function (app) {
//    app.controller('productAddController', productAddController);

//    function productAddController() {

//    }
//})(angular.module('tedushop.products'))

(function (app) {
    app.controller('productAddController',
        ['$scope', 'apiService', 'notificationService', '$state', 'commonService', function ($scope, apiService, notificationService, $state, commonService) {
            //$scope.product = {
            //    CreatedDate: new Date(),
            //    Status: true
            //};

            //$scope.AddProduct = function () {
            //    apiService.post('api/product/create', $scope.product,
            //    function (result) {
            //        notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
            //        $state.go('products');
            //    }, function (error) {
            //        notificationService.displayError('Thêm mới không thành công.');
            //    });
            //};

            //function loadProductCategory() {
            //    apiService.get('/api/productcategory/getallparents', null, function (result) {
            //        $scope.productCategory = result.data;
            //    }), function() {
            //        console.log('Cannot get list parent');
            //    }
            //};

            //loadProductCategory();

            //$scope.GetSeoTitle = function () {
            //    $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
            //};

            //$scope.ckeditorOptions = {
            //    languague: 'vi',
            //    height: '200px'
            //};
         }
    ]);
})(angular.module('tedushop.products'))