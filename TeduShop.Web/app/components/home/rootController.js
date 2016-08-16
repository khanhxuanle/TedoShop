(function(app) {
    app.controller('rootController',
    [
        '$state', 'authData', 'loginService', '$scope', 'authenticationService',
        function($state, authData, loginService, $scope, authenticationService) {
            $scope.logOut = function() {
                loginService.logOut();
                $state.go('login');
            }
            $scope.authentication = authData.authenticationData;

            authenticationService.validateRequest();
        }
    ]);
})(angular.module('tedushop'))