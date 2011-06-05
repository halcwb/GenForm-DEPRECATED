/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/4/11
 * Time: 9:11 PM
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.BrandForm', {
    extend: 'Ext.form.Panel',
    alias: 'widget.brandform',

    initComponent: function () {
        var me = this;
        me.items = me.createItems();

        this.callParent(arguments);
    },

    createItems: function () {
        var items = [
            { xtype: 'textfield',    name:'BrandName',   fieldLabel: 'Merk Naam', margin: '10 0 10 10' }
        ];

        return items;
    },

    getBrand: function () {
        var record = this.getRecord(), me = this;

        me.getForm().updateRecord(record);
        return record;
    }


})