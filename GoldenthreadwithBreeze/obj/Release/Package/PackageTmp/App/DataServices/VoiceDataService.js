/* dataservice: data access and model management layer 
 * relies on Angular injector to provide:
 *     $timeout - Angular equivalent of 'setTimeout'
 *     breeze - the Breeze.Angular service (which is breeze itself)
 *     logger - the application's logging facility
 */
(function () {
    angular.module('app').factory('VoiceDataService',
    ['$http', '$q', '$timeout', 'breeze', 'logger', dataservice]);

    function dataservice($http, $q, $timeout, breeze, logger) {
        var appkey = "73DE4898-C1D3-42A4-B802-0B81F1E3867F";
        var serviceName = 'http://192.168.100.21:91/'; // route to the same origin Web Api controller

        // When data server and application server are in different origins



        var service = {
            GetVoice: GetVoice,
            GetAuthor: GetAuthor,
            GetVoicesByStatus: GetVoicesByStatus,
            GetTags: GetTags
        };
        return service;



        function GetVoice() {
            var host = "api/Voice/";
            var ds = new breeze.DataService({
                serviceName: serviceName + host,
                hasServerMetadata: false
            });
            var manager = new breeze.EntityManager({
                dataService: ds
            });

            var ajaxAdapter = breeze.config.getAdapterInstance('ajax');
            ajaxAdapter.defaultSettings = {
                headers: { "AuthToken": appkey },
            };
            var query = breeze.EntityQuery
                .from("Get")
                .top(10)
                //.where("Title", "startsWith", "A")
                .orderBy("ID");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                alert(error.message);
                return $q.reject(error); // so downstream promise users know it failed
            }
        }


        function GetAuthor() {
            var host = "api/Voice/GetAuthor";
            var ds = new breeze.DataService({
                serviceName: serviceName + host,
                hasServerMetadata: false
            });
            var manager = new breeze.EntityManager({
                dataService: ds
            });

            var ajaxAdapter = breeze.config.getAdapterInstance('ajax');
            ajaxAdapter.defaultSettings = {
                headers: { "AuthToken": appkey },
            };
            var query = breeze.EntityQuery
                .from("GetAuthor")
                .top(10)
                //.where("Title", "startsWith", "A")
                .orderBy("ID");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                alert(error.message);
                return $q.reject(error); // so downstream promise users know it failed
            }
        }

        function GetVoicesByStatus() {
            var host = "api/Voice/GetVoicesByStatus";
            var ds = new breeze.DataService({
                serviceName: serviceName + host,
                hasServerMetadata: false
            });
            var manager = new breeze.EntityManager({
                dataService: ds
            });

            var ajaxAdapter = breeze.config.getAdapterInstance('ajax');
            ajaxAdapter.defaultSettings = {
                headers: { "AuthToken": appkey },
            };
            var query = breeze.EntityQuery
                .from("GetVoicesByStatus")
                .withParameters({ 'Status': 'Pending' })
                .top(10)
                //.where("Title", "startsWith", "A")
                .orderBy("ID");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                alert(error.message);
                return $q.reject(error); // so downstream promise users know it failed
            }
        }




        function GetTags() {
            var host = "api/Voice/GetTags";
            var ds = new breeze.DataService({
                serviceName: serviceName + host,
                hasServerMetadata: false
            });
            var manager = new breeze.EntityManager({
                dataService: ds
            });

            var ajaxAdapter = breeze.config.getAdapterInstance('ajax');
            ajaxAdapter.defaultSettings = {
                headers: { "AuthToken": appkey },
            };
            var query = breeze.EntityQuery
                .from("GetTags")
               // .withParameters({ 'id': 20 })
                .top(10)
                //.where("Title", "startsWith", "A")
                .orderBy("ID");

            var promise = manager.executeQuery(query).catch(queryFailed);
            return promise;

            function queryFailed(error) {
                alert(error.message);
                return $q.reject(error); // so downstream promise users know it failed
            }
        }
    }


})();