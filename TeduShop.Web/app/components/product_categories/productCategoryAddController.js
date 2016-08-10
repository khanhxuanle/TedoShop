(function(app) {
    app.controller('productCategoryAddController',
    [
        '$scope', 'apiService', 'notificationService', '$state', 'commonService', function ($scope, apiService, notificationService, $state, commonService) {
            $scope.productCategory = {
                CreatedDate: new Date(),
                Status: true
            };

            $scope.AddProductCategory = function () {
                apiService.post('api/productcategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('products_category');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
            };

            function loadParentCategory() {
                apiService.get('/api/productcategory/getallparents', null, function(result) {
                    $scope.parentCategories = result.data;
                }), function() {
                    console.log('Cannot get list parent');
                }
            };

            loadParentCategory();

            $scope.GetSeoTitle = function () {
                $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
            };
        }
    ]);
})(angular.module('tedushop.products_category')); 