Ext.define('GenForm.test.store.ShapeNameStoreTests', {
    describe: 'ShapeNameStoreShould',

    tests: function () {
        var me = this, store,
            storeName = 'GenForm.store.product.ShapeName';

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
                directFn: Tests.GetShapeNames
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

        it('return an empty record with ShapeName property', function () {
            var record = store.create();
            expect(record.data.ShapeName).toBeDefined();
        });

        it('contain an item', function () {
            store.add({ShapeName: 'test'});
            expect(store.count() > 0).toBeTruthy();
        });

        it('have an item with ShapeName test', function () {
            expect(store.findExact('ShapeName', 'test') !== -1).toBe(true);
        });

        it('have test direct Fn defined', function () {
           expect(Tests.GetShapeNames).toBeDefined();
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
            }, 'ShapeNameStore to load', GenForm.test.waitingTime);
        });

        it('now contain a shape zetpin', function () {
            expect(store.findExact('ShapeName', 'zetpil') != -1).toBeTruthy();
        });

        it('also contain tablet after load', function () {
            var found = store.getAt(store.findExact('ShapeName', 'tablet'));
            expect(found.data.ShapeName).toBe('tablet');
        });

    }
});