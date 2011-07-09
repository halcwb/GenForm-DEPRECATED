Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true
});

Ext.require([
    'Ext.direct.*',
    'Ext.container.Viewport',
    'Ext.grid.plugin.RowEditing',
    'Ext.form.FieldSet',
    'Ext.tab.Panel',
    'Ext.form.field.HtmlEditor'
]);

Ext.onReady(function () {
    var classTests, componentQueryTests, loaderTests, modelTests,
        storeTests, productControllerTests, brandNameModelTests,
        genericNameModelTests, loginModelTests, productModelTests,
        productSubstanceModelTests, genericNameStoreTests, productSubstanceStoreTests,
        productSubstanceGridTests, saveCancelWindowTests;

    Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

    Ext.app.config.appFolder = '../Client/GenForm/app';
    Ext.app.config.launch = function() {
        var me = this;
        GenForm.application = me;

        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: me.getViewPortItems()
        });

        me.createLoginWindow().show();

        classTests = Ext.create('GenForm.test.extjs.ClassTests');
        describe(classTests.describe, classTests.tests);

        componentQueryTests = Ext.create('GenForm.test.extjs.ComponentQueryTests');
        describe(componentQueryTests.describe, componentQueryTests.tests);

        loaderTests = Ext.create('GenForm.test.extjs.LoaderTests');
        describe(loaderTests.describe, loaderTests.tests);
        
        modelTests = Ext.create('GenForm.test.extjs.ModelTests');
        describe(modelTests.describe, modelTests.tests);
        
        storeTests = Ext.create('GenForm.test.extjs.StoreTests');
        describe(storeTests.describe, storeTests.tests);

        productControllerTests = Ext.create('GenForm.test.controller.ProductControllerTests');
        describe(productControllerTests.describe, productControllerTests.tests);

        brandNameModelTests = Ext.create('GenForm.test.model.BrandNameModelTests');
        describe(brandNameModelTests.describe, brandNameModelTests.tests);

        genericNameModelTests = Ext.create('GenForm.test.model.GenericNameModelTests');
        describe(genericNameModelTests.describe, genericNameModelTests.tests);

        loginModelTests = Ext.create('GenForm.test.model.LoginModelTests');
        describe(loginModelTests.describe, loginModelTests.tests);

        productModelTests = Ext.create('GenForm.test.model.ProductModelTests');
        describe(productModelTests.describe, productModelTests.tests);

        productSubstanceModelTests = Ext.create('GenForm.test.model.ProductSubstanceModelTests');
        describe(productSubstanceModelTests.describe, productSubstanceModelTests.tests);

        genericNameStoreTests = Ext.create('GenForm.test.store.GenericNameStoreTests');
        describe(genericNameStoreTests.describe, genericNameStoreTests.tests);

        productSubstanceStoreTests = Ext.create('GenForm.test.store.ProductSubstanceStoreTests');
        describe(productSubstanceStoreTests.describe, productSubstanceStoreTests.tests);

        productSubstanceGridTests = Ext.create('GenForm.test.view.ProductSubstanceGridTests');
        describe(productSubstanceGridTests.describe, productSubstanceGridTests.tests);

        saveCancelWindowTests = Ext.create('GenForm.test.view.SaveCancelWindowTests');
        describe(saveCancelWindowTests.describe, saveCancelWindowTests.tests);

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
