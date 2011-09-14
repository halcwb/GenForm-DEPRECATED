Ext.define('GenForm.test.extjs.StoreTests', {

    describe: 'Ext.data.StoreTests',

    tests: function () {
        var me = this,
            namespace = 'GenForm.test.extjs.storetests.',
            modelName = namespace + 'TestModel',
            testStore = namespace + 'TestStore',
            isCalledBack = false;

        // Set up test fixture
        Ext.define(modelName, {
            extend: 'Ext.data.Model',

            fields: [
                {name: 'id', type: 'string', mapping: 'ProductId'},
                {name: 'Test', type: 'string', mapping: 'Name'}
            ],

            // I can mover proxy and reader over to store and it keeps working
            // however I cannot alter the config of proxy and/or reader??
            proxy: {
                type: 'direct',
                paramsAsHash: true,
                // If I omit the below line, store test throws an error, but not model tests
                directFn: Tests.GetProduct,
                api: {
                    read: Tests.GetProduct,
                    submit: Tests.SaveProduct
                }
            },
            reader: {
                type: 'direct',
                root: 'data',
                idProperty: 'ProductId'
            }
        });

        Ext.define(testStore, {
            extend: 'Ext.data.Store',
            storeId: 'teststore',
            model: modelName,

            autoLoad: false
        });

        me.createTestStore = function () {
            return Ext.create(testStore);
        };

        it('that a test model is defined', function () {
            expect(Ext.ModelManager.getModel(modelName)).toBeDefined();
        });

        it('that teststore is created', function () {
            expect(me.createTestStore()).toBeDefined();
        });

        it('that teststore can be loaded', function () {
            //noinspection JSUnusedLocalSymbols
            me.createTestStore().load({
                callback: function (records, operation, success) {
                    isCalledBack = true;
                }
            });

            waitsFor(function () {
                return isCalledBack;
            }, 'waiting for loading of teststore', GenForm.test.waitingTime)

        });
    }
});
