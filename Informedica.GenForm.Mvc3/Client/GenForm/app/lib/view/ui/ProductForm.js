Ext.define('GenForm.lib.view.ui.ProductForm', {
    extend: 'GenForm.lib.view.form.FormBase',

    requires: [
        'GenForm.lib.view.component.ComboBoxContainer'
    ],

    height: 498,
    width: 564,
    bodyPadding: 10,
    defaults: {
        anchor: '100%'
    },
    title: 'Bewerk Artikel',

    initComponent: function() {
        var me = this;

        //noinspection JSUnusedGlobalSymbols
        me.fieldDefaults = {
            anchor: '100%',
            height: '22px'
        };

        //noinspection JSUnusedGlobalSymbols
        me.items = [
            {
                xtype: 'fieldset',
                title: 'Artikel Details',
                items: [
                    me.createTextField({
                        itemId: 'fldLabelName',
                        name: 'LabelName',
                        fieldLabel: 'Etiket Naam'
                    }),
                    me.createTextField({
                        itemId: 'fldProductCode',
                        name: 'ProductCode',
                        fieldLabel: 'Artikel Code'
                    }),
                    {
                        xtype: 'comboboxcontainer',
                        editAction: 'editGenericName',
                        addAction: 'addGenericName',
                        comboBox: me.createComboBox({
                            idName: true,
                            itemId: 'cboGenericName',
                            fieldLabel: 'Generiek naam',
                            name: 'GenericName',
                            hideEmptyLabel: false,
                            flex: 1,
                            directFn: GenForm.server.UnitTest.GetGenericNames
                        })
                    },
                    {
                        xtype: 'comboboxcontainer',
                        editAction: 'editBrandName',
                        addAction: 'addBrandName',
                        comboBox: me.createComboBox({
                            idName: true,
                            itemId: 'cboProductBrand',
                            name: 'BrandName',
                            fieldLabel: 'Merk',
                            hideEmptyLabel: false,
                            flex: 1,
                            directFn: GenForm.server.UnitTest.GetBrandNames
                        })
                    },
                    {
                        xtype: 'comboboxcontainer',
                        editAction: 'editShape',
                        addAction: 'addShape',
                        comboBox: me.createComboBox({
                                idName: true,
                                itemId: 'cboProductShape',
                                name: 'Shape',
                                fieldLabel: 'Vorm',
                                hideEmptyLabel: false,
                                flex: 1,
                                directFn: GenForm.server.UnitTest.GetShapeNames
                            })
                    },
                    {
                        xtype: 'comboboxcontainer',
                        extraItems: [
                            me.createNumberField({
                                xtype: 'numberfield',
                                itemId: 'fldProductQuantity',
                                fieldLabel: 'Hoeveelheid',
                                name: 'Quantity',
                                flex: 1
                            })
                        ],
                        editAction: 'editProductUnit',
                        addAction: 'addProductUnit',
                        comboBox:me.createComboBox({
                            idName: true,
                            itemId: 'cboProductUnit',
                            name: 'Unit',
                            flex: 1,
                            directFn: GenForm.server.UnitTest.GetUnitNames
                        })
                    },
                    {
                        xtype: 'comboboxcontainer',
                        editAction: 'editPackage',
                        addAction: 'addPackage',
                        comboBox: me.createComboBox({
                                idName: true,
                                itemId: 'cboProductPackage',
                                name: 'Package',
                                width: 300,
                                fieldLabel: 'Verpakking',
                                hideEmptyLabel: false,
                                flex: 1,
                                directFn: GenForm.server.UnitTest.GetPackageNames
                        })
                    }
                ]
            },
            {
                xtype: 'hiddenfield',
                itemId: 'fldProductId',
                name: 'ProductId',
                fieldLabel: 'Label',
                anchor: '100%'
            },
            {
                xtype: 'tabpanel',
                height: 256,
                itemId: 'tbpProduct',
                activeTab: 0,
                anchor: '100%',
                items: [
                            me.createTab('GenForm.view.product.ProductSubstanceGrid', { name: 'ProductSubstances'}),
                    {
                        xtype: 'gridpanel',
                        title: 'Routes',
                        itemId: 'grdProductRoute',
                        store: me.getProductRouteStore(),
                        viewConfig: {
                            loadMask: true
                        },
                        columns: [
                            {
                                xtype: 'gridcolumn',
                                itemId: 'colRoute',
                                dataIndex: 'Route',
                                text: 'Route',
                                field: {
                                    xtype: 'combobox',
                                    itemId: 'cboProductRoute',
                                    name: 'ProductRoute',
                                    fieldLabel: 'Label',
                                    hideLabel: true,
                                    store: Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetRouteNames })
                                }
                            }
                        ],
                        plugins: [
                            Ext.create('Ext.grid.plugin.RowEditing', {
                                clicksToEdit: 1
                            })
                        ]
                    },
                    {
                        xtype: 'htmleditor',
                        title: 'Bijzonderheden',
                        itemId: 'edtProductText',
                        style: 'background-color: white;',
                        height: 229,
                        name: 'ProductText',
                        fieldLabel: 'Label',
                        hideLabel: true
                    }
                ]
            }
        ];
        me.callParent(arguments);
    }
});