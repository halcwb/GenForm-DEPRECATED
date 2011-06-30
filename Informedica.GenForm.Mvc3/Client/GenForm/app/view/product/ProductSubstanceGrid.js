/**
 * Created by .
 * User: hal
 * Date: 5-5-11
 * Time: 11:41
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.view.product.ProductSubstanceGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.productsubstancegrid',

    initComponent: function () {
        this.store = this.store || this.getProductSubstanceStore();
        this.columns = this.createColumns();
        
        this.callParent(arguments)
    },

    createColumns: function () {
        return  [
            { id: 'ordernumber', header: 'Volgorde', dataIndex: 'OrderNumber', field: 'numberfield'},
            { id: 'genericname', header: 'Generiek', dataIndex: 'GenericName', field: 'textfield'},
            { id: 'quantity', header: 'Hoeveelheid', dataIndex: 'Quantity', field: 'textfield'},
            { id: 'unit', header: 'Eenheid', dataIndex: 'Unit', field: 'textfield'}
        ];
    },

    getProductSubstanceStore: function () {
        return Ext.create('GenForm.store.product.ProductSubstance');
    }
});