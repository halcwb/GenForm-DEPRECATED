Ext.define('GenForm.test.store.SubstanceNameStoreTests', {
    describe: 'SubstanceNameStoreShould',

    tests: function () {
        var me = this, store,
            storeName = 'GenForm.store.common.IdName';

        beforeEach(function () {
            if (!store) store = me.createStore();
        });

        me.createStore = function () {
            return Ext.create(storeName, { directFn: GenForm.server.UnitTest.GetSubstanceNames });
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

        it('return an empty record with SubstanceName property', function () {
            var record = store.create();
            expect(record.data.Name).toBeDefined();
        });

        it('contain an item', function () {
            store.add({Name: 'test'});
            expect(store.count() > 0).toBeTruthy();
        });

        it('have an item with SubstanceName test', function () {
            expect(store.findExact('Name', 'test') !== -1).toBe(true);
        });

        it('have test direct Fn defined', function () {
           expect(GenForm.server.UnitTest.GetSubstanceNames).toBeDefined();
        });

        it('load five test items', function () {
            var result;

            store.load({
                scope   : this,
                callback: function(records) {
                    //the operation object contains all of the details of the load operation
                    result = records;
                }
            });

            waitsFor(function () {
                return result && result.length == 5 || false;
            }, 'SubstanceNameStore to load', GenForm.test.waitingTime);
        });

        it('now contain a Substance paracetamol', function () {
            expect(store.findExact('Name', 'paracetamol') != -1).toBeTruthy();
        });

        it('also contain midazolam after load', function () {
            var found = store.getAt(store.findExact('Name', 'midazolam'));
            expect(found.data.Name).toBe('midazolam');
        });

    }
});