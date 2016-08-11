(function (app) {
    app.controller('productCategoryEditController',
    [
        '$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService', function ($scope, apiService, notificationService, $state, $stateParams, commonService) {
            $scope.productCategory = {
                CreatedDate: new Date(),
                Status: true
            };

            $scope.UpdateProductCategory = function () {
                apiService.put('/api/productcategory/update', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('products_category');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
            };

            function loadParentCategory() {
                apiService.get('/api/productcategory/getallparents', null, function (result) {
                    $scope.parentCategories = result.data;
                }), function () {
                    console.log('Cannot get list parent');
                }
            };

            function loadProductCategoryDetail() {
                apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null,
                    function(result) {
                        $scope.productCategory = result.data;
                    },
                    function(error) {
                        notificationService.displayError(error.data);
                    });
            };

            loadParentCategory();
            loadProductCategoryDetail();

            $scope.GetSeoTitle = function() {
                $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
            };
        }
    ]);
})(angular.module('tedushop.products_category'));