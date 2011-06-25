/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 1:36 PM
 * To change this template use File | Settings | File Templates.
 */
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
    Ext.direct.Manager.addProvider(remoteApi);

    jasmine.getEnv().addReporter(new jasmine.TrivialReporter());
    jasmine.getEnv().execute();

});
