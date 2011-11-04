Ext.define('GenForm.store.common.KeyValue', {
    extend: 'Ext.data.DirectStore',

    model: 'GenForm.model.common.KeyValue',

    constructor: function (config) {
        var me = this;

        if (!config || !config.proxy) {
            Ext.Error.raise('KeyValue store has to be constructed with a proxy');
        }

        me.initConfig(config);
        me.callParent(arguments);
        return me;
    }
});