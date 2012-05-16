Ext.define('GenForm.store.environment.Environment', {
    extend: 'Ext.data.DirectStore',
    storeId: 'environmentStore',
    // This requires is necessary when Ext.Loader is enabled
    requires: ['GenForm.model.environment.Environment'],

    model: 'GenForm.model.environment.Environment',

    constructor: function (config) {
        var me = this;

        if (!config || !config.directFn || !(config.directFn instanceof Function)) {
            config.directFn = GenForm.server.Environment.GetEnvironments;
        }

        config.root = 'data';

        me.initConfig(config);
        me.callParent(arguments);
        return me;
    }
});