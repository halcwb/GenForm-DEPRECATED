Ext.define('GenForm.view.product.SubstanceGrid', {
    extend: 'GenForm.lib.view.ui.SubstanceGrid',
    alias: 'widget.substancegrid',

    initComponent: function () {
        var me = this;

        this.callParent(arguments)
    },

    getSubstanceStore: function () {
        var store =  Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetSubstanceNames});
        //store.load();
        return store;
    }
});