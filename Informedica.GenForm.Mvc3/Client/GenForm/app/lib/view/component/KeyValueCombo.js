Ext.define('GenForm.lib.view.component.KeyValueCombo', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.keyvaluecombo'],

    displayField: 'value',
    valueField: 'value',
    editable: false,
    typeAhead: true,
    queryMode: 'local',

    constructor: function (config) {
        var me = this;

        if (!config || !config.store) {
            Ext.Error.raise('Combobox needs a store');
        }

        me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    }
});