/* dataservice: data access and model management layer 
 * relies on Angular injector to provide:
 *     $timeout - Angular equivalent of 'setTimeout'
 *     breeze - the Breeze.Angular service (which is breeze itself)
 *     logger - the application's logging facility
 */
(function () {
    angular.module('app').factory('ChapterDataService',
    ['$http', '$q', '$timeout', 'breeze', 'logger', dataservice]);

    function dataservice($http, $q, $timeout, breeze, logger) {
        var appkey = "73DE4898-C1D3-42A4-B802-0B81F1E3867F";
        var serviceName = 'http://192.168.100.21:91/'; // route to the same origin Web Api controller

        // When data server and application server are in different origins

       

        var service = {
            GetChapter: GetChapter,
            GetVoiceByChapter: GetVoiceByChapter

        };
        return service;


      
        function GetChapter() {
            var host = "api/Chapters/";
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


        function GetVoiceByChapter() {
            var host = "api/Chapters/GetVoices";
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
                .from("GetVoices")
                .withParameters({ 'id': 20 })
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