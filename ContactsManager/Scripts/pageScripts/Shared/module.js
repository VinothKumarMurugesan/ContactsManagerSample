/// <reference path="../../../Views/Contacts/contactsManager.html" />
/// <reference path="../../../Views/Contacts/contactsManager.html" />
/// <reference path="E:\Velkumar\Projects\TowaERP\_Others\POCs\Towa\Towa\Views/CustomButton.html" />
var app;
var webAPIURL = "http://localhost:9092";
var config = {
    pageSize: 10,
    appServicesHostName: webAPIURL,
    dateFormat: "MM-dd-yyyy",
    dateFormatWithTime: "MM-dd-yyyy hh:mm tt",
    dateFormatWithTime24Hours: "MM-dd-yyyy HH:mm",
    applicationDateFormat: "yyyy-MM-ddTHH:mm:ss"
};

var headers = {
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': ['GET', 'POST', 'OPTIONS', 'PUT', 'DELETE'],
    'Access-Control-Allow-Headers': 'true',
    'Access-Control-Allow-Credendtials': 'true',
    'Content-Type': 'application/json;charset=utf-8'
};

(function () {
    app = angular.module("smEasyModule", ['kendo.directives', 'ngRoute', 'ngStorage', 'ui.bootstrap',
        'SharedServices', 'ngSanitize', 'ngAnimate', 'focus-if',  
        'ui.grid',
    'ui.grid.pagination',
    'ui.grid.edit',
    'ui.grid.resizeColumns',
    'ui.grid.pinning',
    'ui.grid.selection',
    'ui.grid.moveColumns', 'ui.router'])
      .config(function ($routeProvider) {
          $routeProvider
              .when('/index', { templateUrl: '/App/views/contactsManager.html', controller: 'contactController' })
              .otherwise({ redirectTo: '/' });
      });
    app.run(['$rootScope', '$location', '$http', '$localStorage',
    function ($rootScope, $location, $http, $localStorage) {
        $location.path('/index');

    }]);
    app.value("config", config);
})();

app.directive('xenButton', function ($compile) {
    return {
        scope: {
            label: '@', // optional
            click: '&',
            options: '=',
            caption: '@',
            hide: '=',
            disabled: '='
        },
        restrict: 'E',
        replace: true, // optional 
        templateUrl: '../../PartialView/CustomButton.html',
        link: function (scope, element, attr) {
        }
    };
});

app.directive('numeric', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attr, ctrl) {
            function inputValue(val) {
                if (val) {
                    var digits = val.replace(/[^0-9]/g, '');

                    if (digits !== val) {
                        ctrl.$setViewValue(digits);
                        ctrl.$render();
                    }
                    return parseInt(digits, 10);
                }
                else if (val.trim() == '')
                    return true;
                return undefined;
            }
            ctrl.$parsers.push(inputValue);
        }
    };
});

app.directive('disableContents', function () {
    return {
        compile: function (tElem, tAttrs) {

            var inputs = tElem.find('input');
            var textarea = tElem.find('textarea');
            var multiselect = tElem.find('select');
            var button = tElem.find('button');
            var images = tElem.find('image');
            //var aTag = tElem.find('a');
            //aTag.attr('ng-disabled', tAttrs['disableContents']);
            inputs.attr('ng-disabled', tAttrs['disableContents']);
            textarea.attr('ng-disabled', tAttrs['disableContents']);
            multiselect.attr('ng-disabled', tAttrs['disableContents']);
            button.attr('ng-disabled', tAttrs['disableContents']);
            images.attr('ng-disabled', tAttrs['disableContents']);
        }
    }
});

angular.module('SharedServices', []).config(['$httpProvider', function ($httpProvider) {

    $httpProvider.interceptors.push(function ($q, $injector, $rootScope) {
        var $http;
        return { //On Initial Request
            request: function (config) {
                //Loader Is Visible
                $('#mydiv').show();
                $('.loaderClass').removeClass("hideLoader");
                return config;
            },

            response: function (response) { //On Request End
                //$http.pendingRequests Used To Get Pending Request Length               
                $http = $http || $injector.get('$http');
                $rootScope.isRequestCompleted = $http.pendingRequests.length == 0;
                if ($http.pendingRequests.length < 1) {  //If There Is No Length Then There is No Pending Request                       
                    $('#mydiv').hide();
                    $('.loaderClass').addClass("hideLoader");
                }
                return response;
            },
            responseError: function (response) {
                //$http.pendingRequests Used To Get Pending Request Length
                $http = $http || $injector.get('$http');
                if ($http.pendingRequests.length < 1) {    //If There Is No Length Then There is No Request     
                    $('#mydiv').hide();
                    $('.loaderClass').addClass("hideLoader");
                }
                return $q.reject(response);
            }
        }
    });
}
]);

















app.directive('double', function ($parse) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {

            if (!ngModelCtrl) {
                return;
            }

            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }
                var clean = val.replace(/[^0-9.+-]/g, '');
                var decimalCheck = clean.split('.');

                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 8);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }
                var minusCheck = clean.split('-');
                if (!angular.isUndefined(minusCheck[1])) {
                    clean = minusCheck[0] + '-' + minusCheck[1];
                }

                var plusCheck = clean.split('+');
                if (!angular.isUndefined(plusCheck[1])) {
                    clean = plusCheck[0] + '+' + plusCheck[1];
                }

                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });
            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});


app.directive('isNumber', function () {
    return {
        require: 'ngModel',

        link: function (scope, element, attrs) {
            scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                var arr = String(newValue).split("");
                if (arr.length === 0) return;
                if (arr.length === 1 && (arr[0] == '-' || arr[0] === '.' || arr[0] === '+')) return;
                if (arr.length === 2 && newValue === '-+.') return;
                if (isNaN(newValue)) {
                    console.log(oldValue);
                    scope[attrs.ngModel] = oldValue;
                }
            });
        }
    };
});

app.directive('validPassword', function () {
    return {
        require: 'ngModel',
        scope: {
            reference: '=validPassword'
        },
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue, $scope) {
                var noMatch = viewValue != scope.reference
                ctrl.$setValidity('noMatch', !noMatch)
            });
            scope.$watch("reference", function (value) {;
                ctrl.$setValidity('noMatch', value === ctrl.$viewValue);

            });
        }
    }
});


app.directive('passwordStrength', [
    function () {
        return {
            require: 'ngModel',
            restrict: 'E',
            scope: {
                password: '=ngModel'
            },

            link: function (scope, elem, attrs, ctrl) {
                scope.$watch('password', function (newVal) {

                    scope.strength = isSatisfied(newVal && newVal.length >= 8) +
                      isSatisfied(newVal && /[A-Z]/.test(newVal)) +
                      isSatisfied(newVal && /(?=.*\W)/.test(newVal)) +
                      isSatisfied(newVal && /\d/.test(newVal));

                    function isSatisfied(criteria) {
                        return criteria ? 1 : 0;
                    }
                }, true);
            },
            template: '<div class="progress" ng-switch on="strength" ng-show="strength < 1 ? false:true">' +
          '<div ng-switch-when="1" class="progress-bar progress-bar-danger" style="width: 25%">Weak</div>' +
          '<div ng-switch-when="2" class="progress-bar progress-bar-warning" style="width: 50%">Medium</div>' +
          '<div ng-switch-when="3" class="progress-bar progress-bar-warning" style="width:75%">Medium</div>' +
          '<div ng-switch-when="4" class="progress-bar green" style="width: 100%">Strong</div>' +
              '</div>'
        }
    }
]);
app.directive('patternValidator', [
function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, elem, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {
                var patt = new RegExp(attrs.patternValidator);
                var isValid = patt.test(viewValue);
                ctrl.$setValidity('passwordPattern', isValid);

                // angular does this with all validators -> return isValid ? viewValue : undefined;
                // But it means that the ng-model will have a value of undefined
                // So just return viewValue!
                return viewValue;

            });
        }
    };
}
]);

app.directive('uppercase', function (uppercaseFilter, $parse) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            var capitalize = function (inputValue) {
                if (inputValue)
                    var capitalized = inputValue.toUpperCase().replace(new RegExp("[^a-zA-Z0-9\s]", 'g'), '').replace(/\s+/g, '-');

                if (capitalized !== inputValue) {
                    modelCtrl.$setViewValue(capitalized);
                    modelCtrl.$render();
                }
                return capitalized;
            }
            var model = $parse(attrs.ngModel);
            modelCtrl.$parsers.push(capitalize);
            capitalize(model(scope));
        }
    };
});

function ShowMessage($scope, status, message) {
    $scope.Message = message;
    if (status) {
        $scope.show = false;
        $scope.status = "success";
    } else {
        $scope.status = "failed";
    }
};

app.directive('disableContents', function () {
    return {
        compile: function (tElem, tAttrs) {

            var inputs = tElem.find('input');
            var textarea = tElem.find('textarea');
            var multiselect = tElem.find('select');
            var button = tElem.find('button');
            var images = tElem.find('image');
            //var aTag = tElem.find('a');
            //aTag.attr('ng-disabled', tAttrs['disableContents']);
            inputs.attr('ng-disabled', tAttrs['disableContents']);
            textarea.attr('ng-disabled', tAttrs['disableContents']);
            multiselect.attr('ng-disabled', tAttrs['disableContents']);
            button.attr('ng-disabled', tAttrs['disableContents']);
            images.attr('ng-disabled', tAttrs['disableContents']);
        }
    }
});

app.directive("inputDisabled", function () {
    return function (scope, element, attrs) {
        scope.$watch(attrs.inputDisabled, function (val) {
            if (val)
                element.removeAttr("disabled");
            else {
                element.attr("disabled", "disabled");
                element.prop('disabled', true);
            }
        });
    }
});

angular.module('SharedServices', []).config(['$httpProvider', function ($httpProvider) {

    $httpProvider.interceptors.push(function ($q, $injector, $rootScope) {
        var $http;
        return { //On Initial Request
            request: function (config) {
                //Loader Is Visible
                $('#mydiv').show();
                $('.loaderClass').removeClass("hideLoader");
                return config;
            },

            response: function (response) { //On Request End
                //$http.pendingRequests Used To Get Pending Request Length               
                $http = $http || $injector.get('$http');
                $rootScope.isRequestCompleted = $http.pendingRequests.length == 0;
                if ($http.pendingRequests.length < 1) {  //If There Is No Length Then There is No Pending Request                       
                    $('#mydiv').hide();
                    $('.loaderClass').addClass("hideLoader");
                }
                return response;
            },
            responseError: function (response) {
                //$http.pendingRequests Used To Get Pending Request Length
                $http = $http || $injector.get('$http');
                if ($http.pendingRequests.length < 1) {    //If There Is No Length Then There is No Request     
                    $('#mydiv').hide();
                    $('.loaderClass').addClass("hideLoader");
                }
                return $q.reject(response);
            }
        }
    });
}
]);
// maximum value 180 days validation
function isEmpty(value) {
    return angular.isUndefined(value) || value === '' || value === null || value !== value;
}

app.directive('ngMax', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attr, ctrl) {
            scope.$watch(attr.ngMax, function () {
                ctrl.$setViewValue(ctrl.$viewValue);
            });
            var maxValidator = function (value) {
                var max = scope.$eval(attr.ngMax) || Infinity;
                if (!isEmpty(value) && value > max) {
                    ctrl.$setValidity('ngMax', false);
                    return undefined;
                } else {
                    ctrl.$setValidity('ngMax', true);
                    return value;
                }
            };

            ctrl.$parsers.push(maxValidator);
            ctrl.$formatters.push(maxValidator);
        }
    };
});

(function () {
    var __indexOf = [].indexOf || function (item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

    app.directive('chosen', [
       '$timeout', function ($timeout) {
           var CHOSEN_OPTION_WHITELIST, NG_OPTIONS_REGEXP, isEmpty, snakeCase;
           NG_OPTIONS_REGEXP = /^\s*(.*?)(?:\s+as\s+(.*?))?(?:\s+group\s+by\s+(.*))?\s+for\s+(?:([\$\w][\$\w]*)|(?:\(\s*([\$\w][\$\w]*)\s*,\s*([\$\w][\$\w]*)\s*\)))\s+in\s+(.*?)(?:\s+track\s+by\s+(.*?))?$/;
           CHOSEN_OPTION_WHITELIST = ['noResultsText', 'allowSingleDeselect', 'disableSearchThreshold', 'disableSearch', 'enableSplitWordSearch', 'inheritSelectClasses', 'maxSelectedOptions', 'placeholderTextMultiple', 'placeholderTextSingle', 'searchContains', 'singleBackstrokeDelete', 'displayDisabledOptions', 'displaySelectedOptions', 'width'];
           snakeCase = function (input) {
               return input.replace(/[A-Z]/g, function ($1) {
                   return "_" + ($1.toLowerCase());
               });
           };
           isEmpty = function (value) {
               var key;
               if (angular.isArray(value)) {
                   return value.length === 0;
               } else if (angular.isObject(value)) {
                   for (key in value) {
                       if (value.hasOwnProperty(key)) {
                           return false;
                       }
                   }
               }
               return true;
           };
           return {
               restrict: 'A',
               require: '?ngModel',
               terminal: true,
               link: function (scope, element, attr, ngModel) {
                   var chosen, defaultText, disableWithMessage, empty, initOrUpdate, match, options, origRender, removeEmptyMessage, startLoading, stopLoading, valuesExpr, viewWatch;
                   element.addClass('localytics-chosen');
                   options = scope.$eval(attr.chosen) || {};
                   angular.forEach(attr, function (value, key) {
                       if (__indexOf.call(CHOSEN_OPTION_WHITELIST, key) >= 0) {
                           return options[snakeCase(key)] = scope.$eval(value);
                       }
                   });
                   startLoading = function () {
                       return element.addClass('loading').attr('disabled', true).trigger('chosen:updated');
                   };
                   stopLoading = function () {
                       return element.removeClass('loading').attr('disabled', false).trigger('chosen:updated');
                   };
                   chosen = null;
                   defaultText = null;
                   empty = false;
                   initOrUpdate = function () {
                       if (chosen) {
                           return element.trigger('chosen:updated');
                       } else {
                           chosen = element.chosen(options).data('chosen');
                           return defaultText = chosen.default_text;
                       }
                   };
                   removeEmptyMessage = function () {
                       empty = false;
                       return element.attr('data-placeholder', defaultText);
                   };
                   disableWithMessage = function () {
                       empty = true;
                       return element.attr('data-placeholder', chosen.results_none_found).attr('disabled', true).trigger('chosen:updated');
                   };
                   if (ngModel) {
                       origRender = ngModel.$render;
                       ngModel.$render = function () {
                           origRender();
                           return initOrUpdate();
                       };
                       if (attr.multiple) {
                           viewWatch = function () {
                               return ngModel.$viewValue;
                           };
                           scope.$watch(viewWatch, ngModel.$render, true);
                       }
                   } else {
                       initOrUpdate();
                   }
                   attr.$observe('disabled', function () {
                       return element.trigger('chosen:updated');
                   });
                   if (attr.ngOptions && ngModel) {
                       match = attr.ngOptions.match(NG_OPTIONS_REGEXP);
                       valuesExpr = match[7];
                       scope.$watchCollection(valuesExpr, function (newVal, oldVal) {
                           var timer;
                           return timer = $timeout(function () {
                               if (angular.isUndefined(newVal)) {
                                   return startLoading();
                               } else {
                                   if (empty) {
                                       removeEmptyMessage();
                                   }
                                   stopLoading();
                                   if (isEmpty(newVal)) {
                                       return disableWithMessage();
                                   }
                               }
                           });
                       });
                       return scope.$on('$destroy', function (event) {
                           if (typeof timer !== "undefined" && timer !== null) {
                               return $timeout.cancel(timer);
                           }
                       });
                   }
               }
           };
       }
    ]);

}).call(this);
