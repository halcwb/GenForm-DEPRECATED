Ext.define('GenForm.store.common.IdName', {
    extend: 'Ext.data.DirectStore',
    model: 'GenForm.model.common.IdName',

    constructor: function (config) {
        var me = this;

        if (!config || !config.directFn || !(config.directFn instanceof Function)) {
            Ext.Error.raise('IdName store has to be constructed with a valid directFn');
        }

        config.root = 'data';
        config.idProperty = 'Id';

        me.initConfig(config);
        me.callParent(arguments);
        return me;
    }
});