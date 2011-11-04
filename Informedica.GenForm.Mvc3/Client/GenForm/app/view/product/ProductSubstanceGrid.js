Ext.define('GenForm.view.product.ProductSubstanceGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.productsubstancegrid',

    mixins: [
        'GenForm.lib.util.mixin.ButtonCreator'
    ],

    initComponent: function () {
        var me = this;
        me.store = me.store || this.getProductSubstanceStore();

        // ToDo rewrite this, this is ugly!!!
        Ext.applyIf(me, me.createConfig());
        me.callParent(arguments)
    },

    createConfig: function () {
        var me = this, config;

        config = {
            title: 'Stoffen',
            itemId: 'grdProductSubstance',
            store: me.getProductSubstanceStore(),
            dockedItems: [
                {
                    xtype:'toolbar',
                    items: [
                        me.createButton({ action: 'addProductSubstance',    text: 'Voeg artikel stof toe'}),
                        me.createButton({ action: 'deleteProductSubstance', text: 'Verwijder artikel stof', disabled: true})
                    ]
                }
            ],
            columns: [
                {
                    xtype: 'numbercolumn',
                    itemId: 'colSortOrder',
                    width: 67,
                    dataIndex: 'SortOrder',
                    text: 'Volgorde',
                    field: {
                        xtype: 'numberfield',
                        itemId: 'fldSortOrder',
                        name: 'SortOrder'
                    }
                }
                ,
                {
                    xtype: 'gridcolumn',
                    itemId: 'colSubstance',
                    width: 181,
                    dataIndex: 'Substance',
                    text: 'Stofnaam',
                    field: {
                        xtype: 'combobox',
                        itemId: 'cboSubstance',
                        name: 'Substance',
                        store: me.getProductSubstanceStore()
                    }
                },
                {
                    xtype: 'numbercolumn',
                    dataIndex: 'Quantity',
                    text: 'Hoeveelheid',
                    field: {
                        xtype: 'numberfield',
                        itemId: 'fldSubstanceQuantity',
                        name: 'SubstanceQuantity'
                    }
                },
                {
                    xtype: 'gridcolumn',
                    dataIndex: 'Unit',
                    text: 'Eenheid',
                    field: {
                        xtype: 'combobox',
                        itemId: 'cboSubstanceUnit',
                        name: 'SubstanceUnit',
                        store: me.getSubstanceUnitStore()
                    }
                }
            ],
            plugins: [
                Ext.create('Ext.grid.plugin.RowEditing', {
                    clicksToEdit: 1
                })
            ]
        };

        return config;
    },

    getSubstanceUnitStore: function () {
        return Ext.create('GenForm.store.product.Unit')
    },

    getProductSubstanceStore: function () {
        return Ext.create('GenForm.store.product.ProductSubstance');
    }
});