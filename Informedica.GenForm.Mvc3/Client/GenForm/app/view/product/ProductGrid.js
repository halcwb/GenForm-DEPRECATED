Ext.define('GenForm.view.product.ProductGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.productgrid',

    columns: [
        { header: 'Artikel', dataIndex: 'ProductName'}
    ],

    initComponent: function () {
        var me = this;
        me.store = me.store || me.getProductInfoStore();

        me.callParent(arguments)
    },

    getProductInfoStore: function () {
        return Ext.create('GenForm.store.product.Product');
    }
});
