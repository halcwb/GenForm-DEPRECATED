//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.lib.view.form.FormBase', {
    extend: 'Ext.form.Panel',

    height: 600,
    width: 700,

    requires: [
        'GenForm.lib.view.component.EditableComboBox'
    ],
    
    constructor: function (config) {
        var me = this;
        me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    createEditCombo: function (config) {
        var me = this, combo  = { xtype: 'editcombo', queryMode: false, editable: false };

        if (!config.store) throw new Error('['+ Ext.getDisplayName(me) +'] no store defined for ' + config.name);
        
        combo = Ext.apply(combo, config);
        return combo;
    },

    getFormData: function () {
        var me = this,
            record = me.getRecord();

        me.getForm().updateRecord(record);
        return record;
    }
});