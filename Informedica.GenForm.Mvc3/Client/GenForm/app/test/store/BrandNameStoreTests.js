Ext.define('GenForm.test.store.BrandNameStoreTests', {
    describe: 'BrandStoreShould',

    tests: function () {
        var me = this, store,
            storeName = 'GenForm.store.product.BrandName';

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
                directFn: Tests.GetBrandNames
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

        it('return an empty record with BrandName property', function () {
            var record = store.create();
            expect(record.data.Name).toBeDefined();
        });

        it('contain an item', function () {
            store.add({Name: 'test'});
            expect(store.count() > 0).toBeTruthy();
        });

        it('have an item with BrandName test', function () {
            expect(store.findExact('Name', 'test') !== -1).toBe(true);
        });

        it('have test direct Fn defined', function () {
           expect(Tests.GetBrandNames).toBeDefined();
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
            }, 'BrandNameStore to load', GenForm.test.waitingTime);
        });

        it('now contain a brand Dynatra', function () {
            expect(store.findExact('Name', 'Dynatra') != -1).toBeTruthy();
        });

        it('also contain Esmeron after load', function () {
            var found = store.getAt(store.findExact('Name', 'Esmeron'));
            expect(found.data.Name).toBe('Esmeron');
        });

    }
});