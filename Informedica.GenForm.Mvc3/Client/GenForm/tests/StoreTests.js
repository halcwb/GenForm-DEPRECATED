/**
 * Created by .
 * User: halcwb
 * Date: 5/25/11
 * Time: 8:27 AM
 * To change this template use File | Settings | File Templates.
 */
describe('Ext.data.Store', function () {
    var createTestStore;

    Ext.define('TestStoreModel', {
        extend: 'Ext.data.Model',

        fields: [
            {name: 'id', type: 'integer', mapping: 'ProductId'},
            {name: 'Test', type: 'string', mapping: 'ProductName'}
        ]
    });

    Ext.define('TestStore', {
        extend: 'Ext.data.Store',
        storeId: 'teststore',
        model: 'TestStoreModel',

        autoLoad: false,
        proxy: {
            type: 'direct',
            paramsAsHash: true,
            paramOrder: 'id',
            directFn: Product.GetProducts
        },
        reader: {
            type: 'direct',
            root: 'data',
            idProperty: 'ProductId'
        }
    });

    createTestStore = function () {
        return Ext.create('TestStore');
    };

    it('teststore is created', function () {
        expect(createTestStore()).toBeDefined();
    });

    it('teststore can be loaded', function () {
        var isCalledBack = false, me = this;
        createTestStore().load({
            scope: me,
            callback: function (records, operation, success) {
                console.log('is called back');
                isCalledBack = true;
            }
        });
            
        waitsFor(function () {
            return isCalledBack;
        }, 'waiting for loading of teststore', 1000)

    });
});