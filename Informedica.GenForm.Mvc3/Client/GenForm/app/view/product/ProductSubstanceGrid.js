Ext.define('GenForm.view.product.ProductSubstanceGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.productsubstancegrid',

    initComponent: function () {
        var me = this;
        me.store = me.store || this.getProductSubstanceStore();
        me.columns = me.createColumns();
        
        me.callParent(arguments)
    },

    createColumns: function () {
        return  [
            { id: 'ordernumber', header: 'Volgorde',    dataIndex: 'SortOrder', field: 'numberfield'},
            { id: 'genericname', header: 'Generiek',    dataIndex: 'Substance', field: 'textfield'},
            { id: 'quantity',    header: 'Hoeveelheid', dataIndex: 'Quantity',  field: 'textfield'},
            { id: 'unit',        header: 'Eenheid',     dataIndex: 'Unit',      field: 'textfield'}
        ];
    },

    getProductSubstanceStore: function () {
        return Ext.create('GenForm.store.product.ProductSubstance');
    }
});