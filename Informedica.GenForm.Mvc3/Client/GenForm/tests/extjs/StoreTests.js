/**
 * Created by .
 * User: halcwb
 * Date: 5/25/11
 * Time: 8:27 AM
 * To change this template use File | Settings | File Templates.
 */
describe('Ext.data.Store', function () {
    var createTestStore, waitingTime = 200, modelName = 'Test.storetests.TestModel';

    Ext.define(modelName, {
        extend: 'Ext.data.Model',

        fields: [
            {name: 'id', type: 'integer', mapping: 'ProductId'},
            {name: 'Test', type: 'string', mapping: 'ProductName'}
        ],

        // I can mover proxy and reader over to store and it keeps working
        // however I cannot alter the config of proxy and/or reader??
        proxy: {
            type: 'direct',
            paramsAsHash: true,
            // If I omit the below line, store test throws an error, but not model tests
            directFn: Product.GetProduct,
            api: {
                read: Product.GetProduct,
                submit: Product.SaveProduct
            }
        },
        reader: {
            type: 'direct',
            root: 'data',
            idProperty: 'ProductId'
        }
    });

    Ext.define('Test.storetests.TestStore', {
        extend: 'Ext.data.Store',
        storeId: 'teststore',
        model: modelName,

        autoLoad: false
    });

    createTestStore = function () {
        return Ext.create('Test.storetests.TestStore');
    };

    it('a test model is defined', function () {
        expect(Ext.ModelManager.getModel(modelName)).toBeDefined();
    })

    it('teststore is created', function () {
        expect(createTestStore()).toBeDefined();
    });

    it('teststore can be loaded', function () {
        var isCalledBack = false, me = this;
        createTestStore().load({
            scope: me,
            callback: function (records, operation, success) {
                isCalledBack = true;
            }
        });
            
        waitsFor(function () {
            return isCalledBack;
        }, 'waiting for loading of teststore', waitingTime)

    });
});