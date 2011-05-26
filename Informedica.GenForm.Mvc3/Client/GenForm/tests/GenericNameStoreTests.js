/**
 * Created by .
 * User: hal
 * Date: 6-5-11
 * Time: 9:31
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.store.product.GenericName', function() {
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

});