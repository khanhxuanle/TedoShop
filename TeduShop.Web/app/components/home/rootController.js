(function(app) {
    app.controller('rootController',
    [
        '$scope', '$state', function ($scope, $state) {
            $scope.logout = function() {
                $state.go('login');
            };
        }
    ]);
})(angular.module('tedushop'));