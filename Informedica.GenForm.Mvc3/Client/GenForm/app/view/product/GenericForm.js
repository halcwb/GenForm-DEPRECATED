/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/8/11
 * Time: 10:51 AM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.GenericForm', {
    extend: 'Ext.form.Panel',
    alias: 'widget.genericform',

    initComponent: function () {
        var me = this;
        me.items = me.createItems();

        this.callParent(arguments);
    },

    createItems: function () {
        var items = [
            { xtype: 'textfield', name:'GenericName',   fieldLabel: 'Generiek Naam', margin: '10 0 10 10' }
        ];

        return items;
    },

    getGeneric: function () {
        var me = this,
            record = me.getRecord();

        me.getForm().updateRecord(record);
        return record;
    }

});