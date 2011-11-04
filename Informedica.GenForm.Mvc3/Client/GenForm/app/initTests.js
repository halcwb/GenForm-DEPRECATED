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

    //noinspection JSUnusedGlobalSymbols
    Ext.app.config.launch = function() {
        var me = this,
            testList = Ext.create('GenForm.test.TestList'),
            testLoader = Ext.create('GenForm.test.TestLoader');

        GenForm.application = me;

        // Set up app
        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: me.getViewPortItems()
        });

        GenForm.test.waitingTime = 1000;
        GenForm.test.guidGenerator = Ext.create('GenForm.lib.util.GuidGenerator');
        // Load tests
        testLoader.loadTests(testList);

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
