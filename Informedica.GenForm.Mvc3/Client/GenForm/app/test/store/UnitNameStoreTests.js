Ext.define('GenForm.test.store.UnitNameStoreTests', {
    describe: 'UnitNameStoreShould',

    tests: function () {
        var me = this, store,
            storeName = 'GenForm.store.product.UnitName';

        beforeEach(function () {
            if (!store) store = me.createStore();
        });

        me.createStore = function () {
            return Ext.create(storeName);
        };

        me.setUpTestProxy = function () {
            store.setProxy(me.getTestProxy());
        };

        me.getTestProxy = function () {
            return Ext.create('Ext.data.proxy.Direct', {
                type: 'direct',
                directFn: Tests.GetUnitNames
            });
        };

        it('be defined', function () {
            expect(store).toBeDefined();
        });

        it('be defined', function () {
            expect(store).toBeDefined();
        });

        it('to have a direct function', function () {
            expect(store.proxy.directFn).toBeDefined();
        });

        it('return an empty record with UnitName property', function () {
            var record = store.create();
            expect(record.data.UnitName).toBeDefined();
        });

        it('contain an item', function () {
            store.add({UnitName: 'test'});
            expect(store.count() > 0).toBeTruthy();
        });

        it('have an item with UnitName test', function () {
            expect(store.findExact('UnitName', 'test') !== -1).toBe(true);
        });

        it('have test direct Fn defined', function () {
           expect(Tests.GetUnitNames).toBeDefined();
        });

        it('load 2 test items', function () {
            var result;
            me.setUpTestProxy();
            store.load({
                scope   : this,
                callback: function(records) {
                    //the operation object contains all of the details of the load operation
                    result = records;
                }
            });

            waitsFor(function () {
                return result && result.length == 2 || false;
            }, 'UnitNameStore to load', GenForm.test.waitingTime);
        });

        it('now contain a Unit mL', function () {
            expect(store.findExact('UnitName', 'mL') != -1).toBeTruthy();
        });

        it('also contain stuk after load', function () {
            var found = store.getAt(store.findExact('UnitName', 'stuk'));
            expect(found.data.UnitName).toBe('stuk');
        });

    }
});