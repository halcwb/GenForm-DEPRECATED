Ext.require([
    'Ext.direct.*',
    'Ext.container.Viewport',
    'Ext.grid.plugin.RowEditing',
    'Ext.form.FieldSet',
    'Ext.tab.Panel',
    'Ext.form.field.HtmlEditor'
]);

Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true
});

Ext.onReady(function () {
    var newProductTest, loginTest, advancedLoginTest;

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

        advancedLoginTest = Ext.create('GenForm.test.AdvancedLoginTest');
        describe(advancedLoginTest.describe, advancedLoginTest.tests);
        
        loginTest = Ext.create('GenForm.test.LoginTest');
        describe(loginTest.describe, loginTest.tests);

        newProductTest = Ext.create('GenForm.test.NewProductTest');
        describe(newProductTest.describe, newProductTest.tests);

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
