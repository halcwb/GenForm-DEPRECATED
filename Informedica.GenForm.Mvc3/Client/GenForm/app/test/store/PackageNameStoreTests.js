Ext.define('GenForm.test.store.PackageNameStoreTests', {
    describe: 'PackageNameStoreShould',

    tests: function () {
        var me = this, store,
            storeName = 'GenForm.store.product.PackageName',
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
                directFn: Tests.GetPackageNames
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

        it('return an empty record with PackageName property', function () {
            var record = store.create();
            expect(record.data.PackageName).toBeDefined();
        });

        it('contain an item', function () {
            store.add({PackageName: 'test'});
            expect(store.count() > 0).toBeTruthy();
        });

        it('have an item with PackageName test', function () {
            expect(store.findExact('PackageName', 'test') !== -1).toBe(true);
        });

        it('have test direct Fn defined', function () {
           expect(Tests.GetPackageNames).toBeDefined();
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
            }, 'PackageNameStore to load', waitingTime);
        });

        it('now contain a shape ampul', function () {
            expect(store.findExact('PackageName', 'ampul') != -1).toBeTruthy();
        });

        it('also contain tablet after load', function () {
            var found = store.getAt(store.findExact('PackageName', 'tablet'));
            expect(found.data.PackageName).toBe('tablet');
        });

    }
});