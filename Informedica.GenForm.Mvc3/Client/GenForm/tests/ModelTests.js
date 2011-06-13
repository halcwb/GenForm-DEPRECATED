/**
 * Created by .
 * User: halcwb
 * Date: 5/23/11
 * Time: 6:47 PM
 * To change this template use File | Settings | File Templates.
 */


describe('Ext.data.Model', function () {
    var createTestModelInstance, getTestModel, waitingTime = 200;

    Ext.define('Test.modeltests.TestModel', {
        extend: 'Ext.data.Model',

        fields: [
            {name: 'Id', type: 'integer', mapping: 'ProductId'},
            {name: 'Test', type: 'string', mapping: 'ProductName'}
        ],

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

    createTestModelInstance = function () {

        return Ext.ModelManager.create({
            fields: [
                {name: 'Test', type: 'string'}
            ]
        }, 'Test.modeltests.TestModel');
    };

    getTestModel = function () {
        return Ext.ModelManager.getModel('Test.modeltests.TestModel');
    };

    it('a test model should be created', function () {
       expect(createTestModelInstance()).toBeDefined();
    });

    it('test model should have a Test field', function () {
        expect(createTestModelInstance().data.Test).toBeDefined();
    });

    it('test model should have a proxy', function () {
        var model = createTestModelInstance();
        expect(model.getProxy()).toBeDefined();

        model = getTestModel();
        expect(model.getProxy()).toBeDefined();
    });

    it('test model can be loaded using a direct proxy', function () {
        var record, model = getTestModel();

        model.load('123456', {
            callback: function (result) {
                record = result;
            }
        });

        waitsFor(function () {
            return record ? true: false;
        }, 'waiting for Product.GetProduct', waitingTime);
    });

    it('testing the model with a store', function () {
        var result,
            store = Ext.create('Ext.data.DirectStore', {
            model: 'Test.modeltests.TestModel'
        });

        store.setProxy(getTestModel().getProxy());

        // Note, do not pass a selection string like in model.load!!
        store.load({
            callback: function (record) {
                result = record;
            }
        });

        waitsFor(function () {
            return result ? true: false;
        }, 'waiting for loading of store', waitingTime)
    });
});