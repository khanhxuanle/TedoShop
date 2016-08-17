(function(app) {
    app.controller('rootController',
    [
        '$state', 'authData', 'loginService', '$scope', 'authenticationService', '$rootScope',
        function ($state, authData, loginService, $scope, authenticationService, $rootScope) {
            $scope.logOut = function() {
                loginService.logOut();
                $state.go('login');
            }

            $rootScope.authentication = authData.authenticationData;

            authenticationService.validateRequest();
        }
    ]);
})(angular.module('tedushop'))