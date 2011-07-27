Ext.define('GenForm.test.store.SubstanceUnitNameStoreTests', {
    describe: 'SubstanceUnitNameStoreShould',

    tests: function () {
        var me = this, store,
            storeName = 'GenForm.store.product.SubstanceUnitName',
            waitingTime = 200;

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
                directFn: Tests.GetSubstanceUnitNames
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
           expect(Tests.GetSubstanceUnitNames).toBeDefined();
        });

        it('load five test items', function () {
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
                return result && result.length == 5 || false;
            }, 'SubstanceUnitNameStore to load', waitingTime);
        });

        it('now contain a SubstanceUnit mg', function () {
            expect(store.findExact('UnitName', 'mg') != -1).toBeTruthy();
        });

        it('also contain mmol after load', function () {
            var found = store.getAt(store.findExact('UnitName', 'mmol'));
            expect(found.data.UnitName).toBe('mmol');
        });

    }
});