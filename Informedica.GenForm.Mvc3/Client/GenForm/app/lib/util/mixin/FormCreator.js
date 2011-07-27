Ext.define('GenForm.lib.util.mixin.FormCreator', {
    
    createForm: function (config) {
        var me = this,
            formClass = me.getConstructor(config);

        if (!me.forms) me.forms = [];
        me.forms[config.name] = Ext.create(formClass, config);

        return me.forms[config.name];
    },

    getConstructor: function (config) {
        var widget = /^widget\./;
        if (!config.xtype) return 'Ext.form.Panel';

        if (!config.xtype.match(widget)) config.xtype = 'widget.' + config.xtype;
        return config.xtype;
    }
});