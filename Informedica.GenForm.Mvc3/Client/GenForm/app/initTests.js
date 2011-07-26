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
        var me = this, test,
            testList = Ext.create('GenForm.test.TestList');

        GenForm.application = me;

        // Set up app
        Ext.create('Ext.container.Viewport', {
            layout: 'border',
            items: me.getViewPortItems()
        });

        // Load tests
        for (var i = 0; i < testList.tests.length; i++) {
            test = Ext.create(testList.tests[i]);
            describe(test.describe, test.tests);
        }

        jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
        jasmine.Queue(jasmine.getEnv());
        jasmine.getEnv().execute();

    };

    Ext.application(Ext.app.config);

});
