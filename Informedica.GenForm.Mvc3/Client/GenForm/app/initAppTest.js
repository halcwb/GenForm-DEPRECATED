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

    Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

    Ext.app.config.appFolder = '../Client/GenForm/app';

    Ext.app.config.launch = function() {
        var me = this, test,
            testList = Ext.create('GenForm.test.UseCaseList'),
            testLoader = Ext.create('GenForm.test.TestLoader');

        GenForm.application = me;

        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: me.getViewPortItems()
        });

        me.showLoginWindow();
        
        testLoader.loadTests(testList);
        GenForm.test.waitingTime = 500;

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
