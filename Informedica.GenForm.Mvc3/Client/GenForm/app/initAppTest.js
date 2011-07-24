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
    var newProductTest, loginTest, advancedLoginTest, productListTest;

    Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

    Ext.app.config.appFolder = '../Client/GenForm/app';
    Ext.app.config.launch = function() {
        var me = this;
        GenForm.application = me;

        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: me.getViewPortItems()
        });

        me.showLoginWindow();
        
        advancedLoginTest = Ext.create('GenForm.test.usecase.AdvancedLoginTest');
        describe(advancedLoginTest.describe, advancedLoginTest.tests);

        loginTest = Ext.create('GenForm.test.usecase.LoginTest');
        describe(loginTest.describe, loginTest.tests);

        newProductTest = Ext.create('GenForm.test.usecase.NewProductTest');
        describe(newProductTest.describe, newProductTest.tests);

        productListTest = Ext.create('GenForm.test.usecase.ProductListTest');
        describe(productListTest.describe, productListTest.tests);

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
