/**
 * Created by .
 * User: halcwb
 * Date: 5/23/11
 * Time: 6:47 PM
 * To change this template use File | Settings | File Templates.
 */


describe('Ext.data.Model', function () {
    var createTestModelInstance, getTestModel;

    Ext.define('TestDataModel', {
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
        }, 'TestDataModel');
    };

    getTestModel = function () {
        return Ext.ModelManager.getModel('TestDataModel');
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
        }, 'waiting for Product.GetProduct', 1000);
    });

    it('testing the model with a store', function () {
        var store = Ext.create('Ext.data.DirectStore', {
            model: 'TestDataModel'
        });

        // I need to pass the proxy from model to store otherwise I get the error
        console.log(getTestModel().getProxy());
        store.setProxy(getTestModel().getProxy());

        store.load('1', {
            callback: function (result) {
                console.log(result);
            }
        });
    });
});