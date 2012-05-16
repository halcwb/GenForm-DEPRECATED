Ext.define('GenForm.test.store.EnvironmentStoreTests', {
    describe: 'EnvironmentStoreShould',

    tests: function () {
        var me = this, store,
            storeName = 'GenForm.store.environment.Environment';

        beforeEach(function () {
            if (!store) {
                store = me.createStore();
            }
        });

        me.createStore = function () {
            return Ext.create(storeName, { directFn: GenForm.server.UnitTest.GetEnvironments });
        };

        it('be defined', function () {
            expect(store).toBeDefined();
        });

        it('to have a direct function', function () {
            expect(store.proxy.directFn).toBeDefined();
        });

        it('return an empty record with Environment property', function () {
            var record = store.create();
            expect(record.data.Name).toBeDefined();
        });

        it('contain an Environment with name test, a logpath and an export path', function () {
            expect(store.count() == 0).toBeTruthy();
            store.add({Id: '999', Name: 'Test', LogPath: 'c/test/logpath', ExportPath: 'c/test/ExportPath' });
            expect(store.count() > 0).toBeTruthy();
        });

        it('have an item with Environment Test', function () {
            expect(store.findExact('Name', 'Test') !== -1).toBe(true);
        });

        it('have test direct Fn defined', function () {
            expect(GenForm.server.UnitTest.GetEnvironments).toBeDefined();
        });

        it('load 3 test items from the server', function () {
            var result;

            store.load({
                scope   : this,
                callback: function(records) {
                    //the operation object contains all of the details of the load operation
                    result = records;
                }
            });

            waitsFor(function () {
                return result && result.length == 3 || false;
            }, 'Environment store to load', GenForm.test.waitingTime);
        });

        it('now contain an Environment LoadTest', function () {
            expect(store.findExact('Name', 'GenFormTest') != -1).toBeTruthy();
        });


    }
});