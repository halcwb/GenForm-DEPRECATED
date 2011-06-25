/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/25/11
 * Time: 1:05 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.require(['Ext.direct.*'
]);

Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true
});

Ext.onReady(function () {
    Ext.direct.Manager.addProvider(remoteApi);
});

