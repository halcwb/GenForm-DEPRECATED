/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 1:05 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true
});

Ext.require([
    'Ext.direct.*'
]);

Ext.onReady(function () {
    Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

    Ext.app.config.appFolder = './Client/GenForm/app';
    Ext.application(Ext.app.config);
});
