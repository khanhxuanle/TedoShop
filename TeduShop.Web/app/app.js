//var teduapp = angular.module('tedushop', ['ui.router']);

//teduapp.config(function ($stateProvider, $urlRouterProvider) {
//    //
//    // For any unmatched url, redirect to /state1
//    $urlRouterProvider.otherwise("/admin");
//    //
//    // Now set up the states
//    $stateProvider
//      .state('home', {
//          url: "/admin",
//          templateUrl: "/app/components/home/homeView.html",
//          controller: "homeController"
//      });
//});

//(function() {
//    angular.module('tedushop', ['tedushop.common']).config(function($stateProvider, $urlRouterProvider) {
//        //
//        // For any unmatched url, redirect to /state1
//        $urlRouterProvider.otherwise("/admin");
//        //
//        // Now set up the states
//        $stateProvider
//          .state('home', {
//              url: "/admin",
//              templateUrl: "/app/components/home/homeView.html",
//              controller: "homeController"
//          });
//        });
//});

//(function() {
//    angular.module('tedushop', [  'tedushop.products'                               
//                                , 'tedushop.products_category'
//                                , 'tedushop.common']).config(config);

//    config.$inject = ['$stateProvider', '$urlRouterProvider'];

//    function config($stateProvider, $urlRouterProvider) {
//        //
//        // For any unmatched url, redirect to /state1
//        $urlRouterProvider.otherwise("/admin");
//        //
//        // Now set up the states
//        $stateProvider
//          .state('home', {
//              url: "/admin",
//              templateUrl: "/app/components/home/homeView.html",
//              controller: "homeController"
//          });
//    }
//})();

(function () {
    angular.module('tedushop', ['tedushop.products'
                                , 'tedushop.products_category'
                                , 'tedushop.common']).config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
                                    //
                                    // For any unmatched url, redirect to /state1
                                   $urlRouterProvider.otherwise("/admin");
                                    //
                                    // Now set up the states
                                    $stateProvider
                                        .state('home', {
                                            url: "/admin",
                                            templateUrl: "/app/components/home/homeView.html",
                                            controller: "homeController"
                                        });
    }]); 
})();