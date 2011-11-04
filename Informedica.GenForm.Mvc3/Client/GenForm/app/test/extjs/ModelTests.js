Ext.define('GenForm.test.extjs.ModelTests', {
    describe: 'Ext.data.ModelTests',

    tests: function () {
        //noinspection MagicNumberJS
        var me = this, instance,
            namespace = 'GenForm.test.extjs.modeltests.',
            testModel = (namespace + 'TestModel'),
            testModelWithoutProxy = (namespace + 'ModelWithoutStore');

        Ext.define(testModel, {
            extend: 'Ext.data.Model',

            fields: [
                {name: 'Id', type: 'string', mapping: 'ProductId'},
                {name: 'Test', type: 'string', mapping: 'Name'}
            ],

            proxy: {
                type: 'direct',
                paramsAsHash: true,
                // If I omit the below line, store test throws an error, but not model tests
                directFn: GenForm.server.UnitTest.GetProduct,
                api: {
                    read: GenForm.server.UnitTest.GetProduct,
                    submit: GenForm.server.UnitTest.SaveProduct
                }
            },
            reader: {
                type: 'direct',
                root: 'data',
                idProperty: 'ProductId'
            }
        });

        Ext.define(testModelWithoutProxy, {
           extend: 'Ext.data.Model',

            fields: [
                {name: 'Id', type: 'Integer'},
                {name: 'Test', type: 'string'}
            ]
        });

        me.createTestModelInstance = function () {
            if (!instance) instance = Ext.create(testModel);
            return instance;
        };

        me.getTestModel = function () {
            return Ext.ModelManager.getModel(testModel);
        };

        it('that a test model should be created', function () {
           expect(me.createTestModelInstance()).toBeDefined();
        });

        it('that a test model should have a Test field', function () {
            expect(me.createTestModelInstance().data.Test).toBeDefined();
        });

        it('test model should have a proxy', function () {
            var model = me.createTestModelInstance();
            expect(model.getProxy()).toBeDefined();

            model = me.getTestModel();
            expect(model.getProxy()).toBeDefined();
        });

        it('that test model can be loaded using a direct proxy', function () {
            var record, model = me.getTestModel();

            model.load(GenForm.test.guidGenerator.createGuid(), {
                callback: function (result) {
                    record = result;
                }
            });

            waitsFor(function () {
                return record ? true: false;
            }, 'GenForm.server.Tests.GetProduct', GenForm.test.waitingTime);
        });

        it('testing the model with a store', function () {
            var result,
                store = Ext.create('Ext.data.DirectStore', {
                model: testModel,
                proxy: {
                    type: 'direct',
                    directFn: GenForm.server.UnitTest.GetProducts
                }
            });

            store.load({
                callback: function (record) {
                    result = record;
                }
            });

            waitsFor(function () {
                return result ? true: false;
            }, 'waiting for loading of store', GenForm.test.waitingTime)
        });
    }
});