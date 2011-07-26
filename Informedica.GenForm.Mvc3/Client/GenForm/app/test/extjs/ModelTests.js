Ext.define('GenForm.test.extjs.ModelTests', {
    describe: 'Ext.data.Model',

    tests: function () {
        //noinspection MagicNumberJS
        var me = this, instance, waitingTime = 200,
            namespace = 'GenForm.test.extjs.modeltests.',
            testModel = (namespace + 'TestModel'),
            testModelWithoutStore = (namespace + 'ModelWithoutStore');

        Ext.define(testModel, {
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

        Ext.define(testModelWithoutStore, {
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

        it('a test model should be created', function () {
           expect(me.createTestModelInstance()).toBeDefined();
        });

        it('test model should have a Test field', function () {
            expect(me.createTestModelInstance().data.Test).toBeDefined();
        });

        it('test model should have a proxy', function () {
            var model = me.createTestModelInstance();
            expect(model.getProxy()).toBeDefined();

            model = me.getTestModel();
            expect(model.getProxy()).toBeDefined();
        });

        it('test model can be loaded using a direct proxy', function () {
            var record, model = me.getTestModel();

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
                model: testModel
            });

            store.setProxy(me.getTestModel().getProxy());

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
    }
});