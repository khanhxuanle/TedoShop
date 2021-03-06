﻿(function (app) {
    'use strict';

    app.directive('pagerDirective',
        function pagerDirective() {
            return {
                scope: {
                    page: '@',
                    pagesCount: '@',
                    totalCount: '@',
                    searchFunc: '&',
                    customPath: '@'
                },
                replace: true,
                restrict: 'E',
                templateUrl: '/app/shared/directives/pagerDirective.html',
                controller: [
                    '$scope', function($scope) {
                        $scope.search = function(i) {
                            if ($scope.searchFunc) {
                                $scope.searchFunc({ page: i });
                            }
                        };

                        $scope.range = function() {
                            if (!$scope.pagesCount) {
                                return [];
                            }
                            var step = 2;
                            var doubleStep = step * 2;
                            var start = 0;
                            var end = start + 1 + doubleStep;
                            if (end > $scope.pagesCount) {
                                end = $scope.pagesCount;
                            }

                            var ret = [];
                            for (var i = start; i != end; ++i) {
                                ret.push(i);
                            }

                            return ret;
                        };

                        $scope.pagePlus = function(count) {
                            return +$scope.page + count;
                        }

                    }
                ]
            }
        });

})(angular.module('tedushop.common'));