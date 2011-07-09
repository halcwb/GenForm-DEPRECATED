Ext.define('GenForm.test.store.GenericNameStoreTests', {

    describe: 'GenForm.store.product.GenericName',

    tests: function() {
        var getGenericNameStore, createGenericNameStore, addItemToGenericNameStore;

        getGenericNameStore = function () {
            return Ext.getStore('genericnamestore');
        };

        addItemToGenericNameStore = function () {
            getGenericNameStore().add({ GenericName: 'test' });
        };

        createGenericNameStore = function () {
            return Ext.create('GenForm.store.product.GenericName');
        }

        it('GenForm.store.product.GenericNameStore can be created', function () {
            expect(createGenericNameStore()).toBeDefined();
        })

        it('GenForm.store.product.GenericNameStore should be defined', function () {
            expect(getGenericNameStore()).toBeDefined();
        });

        it('GenericNameStore should contain an item', function () {
            addItemToGenericNameStore();
            expect(getGenericNameStore().count() > 0).toBe(true);
        });

        it('GenericNameStore contains an item with GenericName test', function () {
            expect(getGenericNameStore().findExact('GenericName', 'test') !== -1).toBe(true);
        });

    }
});