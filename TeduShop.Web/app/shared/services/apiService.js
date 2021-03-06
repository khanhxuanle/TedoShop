﻿//(function(app) {
//    app.service('apiService', apiService);

//    apiService.$inject = ["$http"];

//    function apiService($http) {
//        return {
//            get: get
//        }

//        function  get(url, params, success, failure) {
//            $http.get(url, params)
//                .then(function(result) {
//                        success(result);
//                    },
//                    function(error) {
//                        failure(error);
//                    });
//        }
//    }
//})(angular.module('tedushop.common'));

(function (app) {
    app.service('apiService', ['$http', 'notificationService', 'authenticationService', function ($http, notificationService, authenticationService) {
        function get(url, params, success, failure) {
            authenticationService.setHeader();
            $http.get(url, params)
                .then(function (result) {
                    success(result);
                },
                    function (error) {
                        console.log(error.status);
                        if (error.status === 401) {
                            notificationService.displayError('Authenticate is required.');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                         
                    });
        };

        function post(url, data, success, failure) {
            authenticationService.setHeader();
            $http.post(url, data)
                .then(function(result) {
                        success(result);
                    },
                    function (error) {
                        console.log(error.status);
                        if (error.status === 401) {
                            notificationService.displayError('Authenticate is required.');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        };

        function put(url, data, success, failure) {
            authenticationService.setHeader();
            $http.put(url, data)
                .then(function (result) {
                    success(result);
                },
                    function (error) {
                        console.log(error.status);
                        if (error.status === 401) {
                            notificationService.displayError('Authenticate is required.');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        };

        function del(url, data, success, failure) {
            authenticationService.setHeader();
            $http.delete(url, data)
                .then(function (result) {
                    success(result);
                },
                    function (error) {
                        console.log(error.status);
                        if (error.status === 401) {
                            notificationService.displayError('Authenticate is required.');
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        };

        return {
            get: get,
            post: post,
            put: put,
            del: del
        };

    }]);
})(angular.module('tedushop.common'));