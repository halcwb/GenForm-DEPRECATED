Ext.define('GenForm.lib.view.component.ComboBoxContainer', {
    extend: 'Ext.form.FieldContainer',
    alias: ['widget.combocontainer', 'widget.comboboxcontainer'],

    height: 25,
    width: 400,
    layout: {
        align: 'stretch',
        type: 'hbox'
    },
    hideLabel: true,

    editAction: undefined,
    addAction: undefined,
    comboBox: undefined,

    constructor: function (config) {
        var me = this;

        if (config.extraItems) me.extraItems = config.items;
        me.initConfig(config);

        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        me.initComboBoxContainer();
        me.callParent(arguments);
    },

    initComboBoxContainer: function () {
        var me = this, items;

        if (!me.editAction || !me.addAction || !me.comboBox) {
            Ext.Error.raise('Configuration of ComboBoxContainer incomplete');
        }

        items = [
            me.comboBox,
            {
                xtype: 'button',
                action: me.editAction,
                height: 19,
                width: 70,
                text: 'Bewerk'
            },
            {
                xtype: 'button',
                action: me.addAction,
                height: 19,
                width: 70,
                text: 'Nieuw'
            }
        ];

        me.items = me.extraItems ? me.extraItems.concat(items) : items;
    }

});