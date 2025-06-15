<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Project_Test.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        table.repeater-table {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, sans-serif;
        }

            table.repeater-table th,
            table.repeater-table td {
                border: 1px solid #ccc;
                padding: 8px 10px;
                text-align: center;
            }

            table.repeater-table th {
                background-color: #007BFF;
                color: white;
            }

            table.repeater-table tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            table.repeater-table tr:hover {
                background-color: #f1f1f1;
            }

        .action-link {
            color: #007BFF;
            text-decoration: none;
            margin: 0 5px;
        }

            .action-link:hover {
                text-decoration: underline;
            }
    </style>

    <div id="viewRiwayatProduksi" runat="server">
        <h3>Riwayat Perataan Produksi</h3>
        <asp:Button ID="btnAddForm" runat="server" Text="Add Form" OnClick="btnAddForm_Click" />
        <br />
        <br />
        <asp:Repeater ID="rptRiwayat" runat="server" OnItemCommand="rptRiwayat_ItemCommand">
            <HeaderTemplate>
                <table border="1" cellpadding="5" cellspacing="0" class="repeater-table">
                    <tr style="background-color: #f0f0f0;">
                        <th>Tanggal</th>
                        <th>Senin</th>
                        <th>Selasa</th>
                        <th>Rabu</th>
                        <th>Kamis</th>
                        <th>Jumat</th>
                        <th>Sabtu</th>
                        <th>Minggu</th>
                        <th>Senin (Fix)</th>
                        <th>Selasa (Fix)</th>
                        <th>Rabu (Fix)</th>
                        <th>Kamis (Fix)</th>
                        <th>Jumat (Fix)</th>
                        <th>Sabtu (Fix)</th>
                        <th>Minggu (Fix)</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("dCreatedAt", "{0:dd-MM-yyyy HH:mm}") %></td>
                    <td><%# Eval("iSenin") %></td>
                    <td><%# Eval("iSelasa") %></td>
                    <td><%# Eval("iRabu") %></td>
                    <td><%# Eval("iKamis") %></td>
                    <td><%# Eval("iJumat") %></td>
                    <td><%# Eval("iSabtu") %></td>
                    <td><%# Eval("iMinggu") %></td>
                    <td><%# Eval("iSeninFix") %></td>
                    <td><%# Eval("iSelasaFix") %></td>
                    <td><%# Eval("iRabuFix") %></td>
                    <td><%# Eval("iKamisFix") %></td>
                    <td><%# Eval("iJumatFix") %></td>
                    <td><%# Eval("iSabtuFix") %></td>
                    <td><%# Eval("iMingguFix") %></td>
                    <td style='<%# Convert.ToInt32(Eval("iIsActive")) == 1 ? "color:green;": "color:red;" %>'>
                        <%# Convert.ToInt32(Eval("iIsActive")) == 1 ? "Aktif" : "Tidak Aktif" %>
                    </td>
                    <td>                        
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="hapus" CommandArgument='<%# Eval("iId") %>' OnClientClick="return confirm('Yakin ingin menghapus data ini?');">Delete</asp:LinkButton>
                    </td>

                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div id="formAdd" runat="server" visible="false">
        <div>
            <h2>Input Produksi Mobil (7 Hari Kerja)</h2>
            <asp:Label ID="lblTanggal" runat="server" Text="Tanggal Rencana Produksi:"></asp:Label><br />
            <asp:TextBox ID="txtTanggalProduksi" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtSenin" runat="server" placeholder="Senin"></asp:TextBox>
            <asp:TextBox ID="txtSelasa" runat="server" placeholder="Selasa"></asp:TextBox>
            <asp:TextBox ID="txtRabu" runat="server" placeholder="Rabu"></asp:TextBox>
            <asp:TextBox ID="txtKamis" runat="server" placeholder="Kamis"></asp:TextBox>
            <asp:TextBox ID="txtJumat" runat="server" placeholder="Jumat"></asp:TextBox>
            <asp:TextBox ID="txtSabtu" runat="server" placeholder="Sabtu"></asp:TextBox>
            <asp:TextBox ID="txtMinggu" runat="server" placeholder="Minggu"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnProses" runat="server" Text="Proses" OnClick="btnProses_Click" />
            <br />
            <br />
            <asp:Literal ID="ltHasil" runat="server"></asp:Literal>
            <br />
            <asp:Button ID="btnSimpan" runat="server" OnClick="btnSimpan_Click" Text="Simpan" Visible="false" />
        </div>
    </div>
</asp:Content>
